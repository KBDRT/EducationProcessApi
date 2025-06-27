using EducationProcessAPI.Application.Services.CRUD.Definition;
using EducationProcessAPI.Application.Services.CRUD.Implementation;
using EducationProcessAPI.Application.Services.Helpers.Definition;
using EducationProcessAPI.Application.Services.Helpers.Implementation;
using EducationProcessAPI.Application.ServiceUtils;
using EducationProcessAPI.Application.Abstractions.Repositories;
using EducationProcessAPI.Infrastructure.DataBase.Repositories.Implementation;
using EducationProcessAPI.Application.Parsers;
using EducationProcessAPI.Infrastructure.Files.Parsers;
using EducationProcessAPI.Domain.Entities;


namespace EducationProcess.Presentation
{
    public static class Extensions
    {
        public static void AddServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddTransient<IUnionService, UnionService>();
            builder.Services.AddTransient<ITeacherService, TeacherService>();
            builder.Services.AddTransient<IDirectionService, DirectionService>();
            builder.Services.AddTransient<IGroupService, GroupService>();
            builder.Services.AddTransient<ILessonService, LessonService>();
            builder.Services.AddTransient<IAnalysisService, AnalysisService>();

            builder.Services.AddTransient<ITeacherRepository, TeacherRepository>();
            builder.Services.AddTransient<IUnionRepository, UnionRepository>();
            builder.Services.AddTransient<IDirectionRepository, DirectionRepository>();
            builder.Services.AddTransient<IGroupRepository, GroupRepository>();
            builder.Services.AddTransient<ILessonRepository, LessonRepository>();
            builder.Services.AddTransient<IAnalysisRepository, AnalysisRepository>();

            builder.Services.AddTransient<IParseFile<Group>, WordParseLesson>();

            builder.Services.AddTransient<IOperationResultService, OperationResultService>();

        }

        public static int GetStatusCodeByOperationStatus(this AppOperationStatus status)
        {
            return status switch
            {
                AppOperationStatus.Success => StatusCodes.Status200OK,
                AppOperationStatus.Error => StatusCodes.Status500InternalServerError,
                AppOperationStatus.NotFound => StatusCodes.Status404NotFound,
                _ => StatusCodes.Status400BadRequest,
            };
        }
    }
}
