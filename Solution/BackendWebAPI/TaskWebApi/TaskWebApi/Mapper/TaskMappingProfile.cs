using AutoMapper;
using TaskWebApi.Classes;
using TaskWebApi.DTO;

namespace TaskWebApi.Mapper
{
    public class TaskMappingProfile : Profile
    {
        public TaskMappingProfile()
        {
            CreateMap<TaskItem, TaskDto>();
            CreateMap<User, UserDto>();
        }
    }

}
