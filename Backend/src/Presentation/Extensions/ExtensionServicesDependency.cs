using Application.Abstractions.Repositories;
using Application.Cache.Definition;
using Application.Cache.Implementation;
using Application.CQRS.Analysis.Commands.CreateCriteria;
using Application.CQRS.Analysis.Commands.CreateOption;
using Application.CQRS.Teachers.Commands.CreateTeacher;
using Application.CQRS.Teachers.Commands.UpdateTeacher;
using Application.DTO;
using Application.Validators.Base;
using Application.Validators.CRUD;
using Application.Validators.CRUD.Create;
using Application.Validators.CRUD.General;
using EducationProcess.Presentation.Contracts;
using EducationProcessAPI.Application.Abstractions.Repositories;
using EducationProcessAPI.Application.DTO;
using EducationProcessAPI.Application.Parsers;
using EducationProcessAPI.Application.Services.CRUD.Definition;
using EducationProcessAPI.Application.Services.CRUD.Implementation;
using EducationProcessAPI.Domain.Entities;
using EducationProcessAPI.Domain.Entities.LessonAnalyze;
using EducationProcessAPI.Infrastructure.DataBase.Repositories.Implementation;
using EducationProcessAPI.Infrastructure.Files.Parsers;
using FluentValidation;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SpectreConsole;

namespace Presentation.Extensions
{
    public static class ExtensionServicesDependency
    {
        public static void AddDependency(this IServiceCollection services)
        {
            AddServices(services);
            AddValidators(services);
            AddRepositories(services);
            AddParsers(services);
            AddSerilogCustom(services);
            AddCaches(services);
        }

        private static void AddServices(IServiceCollection services)
        {
            services.AddScoped<IUnionService, UnionService>();
            services.AddScoped<IDirectionService, DirectionService>();
            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<ILessonService, LessonService>();
        }

        private static void AddCaches(IServiceCollection services)
        {
            services.AddScoped<ICacheManagerFactory, CacheManagerFactory>();
        }


        private static void AddValidators(IServiceCollection services)
        {
            services.AddScoped<IValidatorFactoryCustom, ValidatorFactory>();

            services.AddScoped<IValidator<IFormFile>, UploadFileValidator>();
            services.AddScoped<IValidator<Guid>, GuidEmptyValidator>();

            services.AddScoped<IValidator<CreateCriteriaCommand>, CreateCriteriaValidator>();
            services.AddScoped<IValidator<CreateDirectionDto>, CreateDirectionValidator>();
            services.AddScoped<IValidator<CreateGroupFromFileDto>, CreateGroupFromFileValidator>();
            services.AddScoped<IValidator<CreateGroupDto>, CreateGroupValidator>();
            services.AddScoped<IValidator<LessonDto>, CreateLessonValidator>();
            services.AddScoped<IValidator<CreateOptionCommand>, CreateOptionValidator>();
            services.AddScoped<IValidator<CreateTeacherCommand>, CreateTeacherValidator>();
            services.AddScoped<IValidator<CreateUnionDto>, CreateUnionValidator>();
            services.AddScoped<IValidator<UpdateTeacherCommand>, UpdateTeacherValidator>();
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<ITeacherRepository, TeacherRepository>();
            services.AddScoped<IUnionRepository, UnionRepository>();
            services.AddScoped<IDirectionRepository, DirectionRepository>();
            services.AddScoped<IGroupRepository, GroupRepository>();
            services.AddScoped<ILessonRepository, LessonRepository>();
            services.AddScoped<IAnalysisRepository, AnalysisRepository>();
            services.AddScoped<IAuthRepository, AuthRepository>();
        }

        private static void AddParsers(IServiceCollection services)
        {
            services.AddTransient<IParseFile<Group>, WordParseLesson>();
            services.AddTransient<IParseFile<AnalysisCriteria>, WordParserGrades>();
        }

        public static void AddSerilogCustom(IServiceCollection services)
        {

            Log.Logger = new LoggerConfiguration()
                .WriteTo.SpectreConsole("{Timestamp:HH:mm:ss} [{Level}] {Message}{NewLine}{Exception}")
                .MinimumLevel.Verbose()
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Information)
                .MinimumLevel.Override("Microsoft.AspNetCore.Hosting", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.AspNetCore.Mvc", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.AspNetCore.Routing", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.AspNetCore.Cors", LogEventLevel.Warning)
                .CreateLogger();

            services.AddSerilog();
        }

    }
}
