using Application.DTO;
using Application.Validators.Base;
using Application.Validators.CRUD;
using Application.Validators.CRUD.Create;
using Application.Validators.CRUD.General;
using Application.Validators.CRUD.Update;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
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

namespace EducationProcess.Presentation
{
    public static class Extensions
    {
        public static void AddDependency(this WebApplicationBuilder builder)
        {
            AddServices(builder);
            AddValidators(builder);
            AddRepositories(builder);
            AddParsers(builder);
        }



        private static void AddServices(WebApplicationBuilder builder)
        {
            builder.Services.AddTransient<IUnionService, UnionService>();
            builder.Services.AddTransient<ITeacherService, TeacherService>();
            builder.Services.AddTransient<IDirectionService, DirectionService>();
            builder.Services.AddTransient<IGroupService, GroupService>();
            builder.Services.AddTransient<ILessonService, LessonService>();
            builder.Services.AddTransient<IAnalysisService, AnalysisService>();
        }

        private static void AddValidators(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<Application.Validators.Base.IValidatorFactoryCustom, ValidatorFactory>();

            builder.Services.AddScoped<IValidator<IFormFile>, UploadFileValidator>();
            builder.Services.AddScoped<IValidator<Guid>, GuidEmptyValidator>();

            builder.Services.AddScoped<IValidator<CreateAnalysisCriteriaDto>, CreateCriteriaValidator>();
            builder.Services.AddScoped<IValidator<CreateDirectionDto>, CreateDirectionValidator>();
            builder.Services.AddScoped<IValidator<CreateGroupFromFileDto>, CreateGroupFromFileValidator>();
            builder.Services.AddScoped<IValidator<CreateGroupDto>, CreateGroupValidator>();
            builder.Services.AddScoped<IValidator<LessonDto>, CreateLessonValidator>();
            builder.Services.AddScoped<IValidator<CreateOptionDto>, CreateOptionValidator>();
            builder.Services.AddScoped<IValidator<CreateTeacherDto>, CreateTeacherValidator>();
            builder.Services.AddScoped<IValidator<CreateUnionDto>, CreateUnionValidator>();
            builder.Services.AddScoped<IValidator<TeacherDto>, UpdateTeacherValidator>();
        }


        private static void AddRepositories(WebApplicationBuilder builder)
        {
            builder.Services.AddTransient<ITeacherRepository, TeacherRepository>();
            builder.Services.AddTransient<IUnionRepository, UnionRepository>();
            builder.Services.AddTransient<IDirectionRepository, DirectionRepository>();
            builder.Services.AddTransient<IGroupRepository, GroupRepository>();
            builder.Services.AddTransient<ILessonRepository, LessonRepository>();
            builder.Services.AddTransient<IAnalysisRepository, AnalysisRepository>();
        }

        private static void AddParsers(WebApplicationBuilder builder)
        {
            builder.Services.AddTransient<IParseFile<Group>, WordParseLesson>();
            builder.Services.AddTransient<IParseFile<AnalysisCriteria>, WordParserGrades>();
        }

    }
}
