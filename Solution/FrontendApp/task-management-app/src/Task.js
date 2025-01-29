import React, { useEffect, useState } from "react";
import axios from "axios";
import { useParams } from "react-router-dom";
import "./TaskComponent.css";

const TaskComponent = () => {
    const { userId } = useParams();
    const [tasks, setTasks] = useState([]);
    const [userName, setUserName] = useState("");
    const [filter, setFilter] = useState("all");
    const [isLoading, setIsLoading] = useState(false);
    const [message, setMessage] = useState("");

    useEffect(() => {
        fetchTasks(userId);
    }, [userId]);

    const fetchTasks = async (userId) => {
        setIsLoading(true);

        axios.get(`http://localhost/api/Task/${userId}`)
            .then((response) => {
                setTasks(response.data.tasks);
                setUserName(`${response.data.user.firstName} ${response.data.user.lastName}`);
                setMessage("");
            }).catch(() => {
                setMessage("Error fetching tasks. Please try again later.");
            }).finally(() => {
                setIsLoading(false);
            });
    };

    const updateTaskStatus = async (taskId, newStatus) => {
        setIsLoading(true);
        const taskData = { userId, taskId, newStatus };

        axios.put(`http://localhost/api/Task`, taskData).then(() => {
            setTasks((prevTasks) =>
                prevTasks.map((task) =>
                    task.taskId === taskId ? { ...task, status: newStatus } : task
                )
            );
            setMessage("Task status updated successfully.");
        }).catch(() => {
            setMessage("Error updating task. Please try again later.");
        }).finally(() => {
            setIsLoading(false);
        });
    };

    const filteredTasks = tasks.filter((task) => {
        if (filter === "all") return true;
        return filter === "completed" ? task.status : !task.status;
    });

    return (
        <div className="task-container">
            <h1 className="title">Welcome, {userName}</h1>
            <label htmlFor="filter" className="label">Filter Tasks:</label>
            <select
                id="filter"
                value={filter}
                onChange={(e) => setFilter(e.target.value)}
                className="select"
            >
                <option value="all">All</option>
                <option value="completed">Completed</option>
                <option value="pending">Pending</option>
            </select>

            {isLoading ? (
                <p className="loading">Loading...</p>
            ) : (
                <table className="task-table">
                    <thead>
                        <tr>
                            <th>Task Name</th>
                            <th>Status</th>
                            <th>Action</th>
                        </tr>
                    </thead>
                    <tbody>
                        {filteredTasks.map((task) => (
                            <tr key={task.taskId}>
                                <td>{task.taskName}</td>
                                <td>{task.status ? "Completed" : "Pending"}</td>
                                <td>
                                    <input
                                        type="checkbox"
                                        checked={task.status}
                                        onChange={() => updateTaskStatus(task.taskId, !task.status)}
                                        disabled={isLoading}
                                    />
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            )}

            {message && <p className="message">{message}</p>}
        </div>
    );
};

export default TaskComponent;
