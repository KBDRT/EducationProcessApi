using Application.Abstractions.Repositories;
using Application.Auth.Policy.UploadEduPlan;
using Application.Cache.Definition;
using Application.Cache.Implementation;
using Application.CQRS.Analysis.Commands.CreateCriteria;
using Application.CQRS.Analysis.Commands.CreateOption;
using Application.CQRS.Auth.Commands.RegisterUser;
using Application.CQRS.Teachers.Commands.CreateTeacher;
using Application.CQRS.Teachers.Commands.UpdateTeacher;
using Application.DTO;
using Application.Helpers;
using Application.Mapping;
using Application.Services.Helpers.Definition;
using Application.Services.Helpers.Implementation;
using Application.Validators.Base;
using Application.Validators.CRUD;
using Application.Validators.CRUD.Create;
using Application.Validators.CRUD.General;
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
using Infrastructure.Background;
using Infrastructure.DataBase.Repositories;
using Microsoft.AspNetCore.Authorization;
using Presentation.Mapping;

namespace Presentation.Extensions
{
    public static class ExtensionServicesDependency
    {
        private static IServiceCollection _services = null!;

        public static void AddDependency(this IServiceCollection services)
        {
            _services = services;

            AddHelpers();
            AddPolicy();
            AddServices();
            AddValidators();
            AddRepositories();
            AddParsers();
            AddCaches();
            AddOthers();
        }

        private static void AddServices()
        {
            _services.AddScoped<IUnionService, UnionService>();
            _services.AddScoped<IDirectionService, DirectionService>();
            _services.AddScoped<IGroupService, GroupService>();
            _services.AddScoped<ILessonService, LessonService>();

            _services.AddScoped<StatisticService, TableStatisticService>();
        }

        private static void AddCaches()
        {
            _services.AddScoped<ICacheManagerFactory, CacheManagerFactory>();
        }


        private static void AddValidators()
        {
            _services.AddScoped<IValidatorFactoryCustom, ValidatorFactory>();

            _services.AddScoped<IValidator<IFormFile>, UploadFileValidator>();
            _services.AddScoped<IValidator<Guid>, GuidEmptyValidator>();

            _services.AddScoped<IValidator<CreateCriteriaCommand>, CreateCriteriaValidator>();
            _services.AddScoped<IValidator<CreateDirectionDto>, CreateDirectionValidator>();
            _services.AddScoped<IValidator<CreateGroupFromFileDto>, CreateGroupFromFileValidator>();
            _services.AddScoped<IValidator<CreateGroupDto>, CreateGroupValidator>();
            _services.AddScoped<IValidator<LessonDto>, CreateLessonValidator>();
            _services.AddScoped<IValidator<CreateOptionCommand>, CreateOptionValidator>();
            _services.AddScoped<IValidator<CreateTeacherCommand>, CreateTeacherValidator>();
            _services.AddScoped<IValidator<CreateUnionDto>, CreateUnionValidator>();
            _services.AddScoped<IValidator<UpdateTeacherCommand>, UpdateTeacherValidator>();
            _services.AddScoped<IValidator<RegisterUserCommand>, RegisterUserValidator>();
        }

        private static void AddRepositories()
        {
            _services.AddScoped<ITeacherRepository, TeacherRepository>();
            _services.AddScoped<IUnionRepository, UnionRepository>();
            _services.AddScoped<IDirectionRepository, DirectionRepository>();
            _services.AddScoped<IGroupRepository, GroupRepository>();
            _services.AddScoped<ILessonRepository, LessonRepository>();
            _services.AddScoped<IAnalysisRepository, AnalysisRepository>();
            _services.AddScoped<IAuthRepository, AuthRepository>();
            _services.AddScoped<IStatisticsRepository, StatiscRepository>();

            _services.AddScoped<IBaseRepository<Lesson>, BaseRepository<Lesson>>();
        }

        private static void AddParsers()
        {
            _services.AddTransient<IParseFile<Group>, WordParseLesson>();
            _services.AddTransient<IParseFile<AnalysisCriteria>, WordParserGrades>();
        }

        private static void AddHelpers()
        {
            _services.AddScoped<JwtTokenGenerator>();
        }

        private static void AddPolicy()
        {
            _services.AddTransient<IAuthorizationHandler, RoleRequirementHandler>();
        }

        private static void AddOthers()
        {
            _services.AddAutoMapper(typeof(MappingProfileDto));
            _services.AddAutoMapper(typeof(MappingProfileRequest));

            _services.AddHostedService<StatisticsFormerBackgroundService>();
        }

    }
}
