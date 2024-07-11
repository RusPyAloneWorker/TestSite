using AutoMapper;
using TestSite.Domain.TestResultRoot;
using TestSite.Domain.TestRoot;
using TestSite.Infrastructure.DatabaseModels;
using Test = TestSite.Domain.TestRoot.Test;

namespace TestSite.Mappings;

public class DomainAndDbModelMapping: Profile
{
	public DomainAndDbModelMapping()
	{
		CreateMap<Test, TestModel>()
			.ReverseMap();
		
		CreateMap<Question, QuestionModel>()
			.ForMember(dest => dest.QuestionOptions, 
				opt => opt.MapFrom(src => src.Answers))
			.ForMember(dest => dest.HasMultipleAnswers, 
				opt => opt.MapFrom(src => src.HasMultipleAnswers))
			.ForMember(dest => dest.Text, 
				opt => opt.MapFrom(src => src.Text))
			.ForMember(dest => dest.Id, 
				opt => opt.MapFrom(src => src.Id))
			.ReverseMap();
		
		CreateMap<QuestionOption, QuestionOptionModel>()
			.ReverseMap();

		CreateMap<AnsweredQuestion, AnsweredQuestionModel>()
			.ForMember(dest => dest.Id,
				opt => opt.MapFrom(src => src.Id))
			.ForMember(dest => dest.QuestionOptions,
				opt => opt.MapFrom(src => src.QuestionOptions))
			.ForMember(dest => dest.QuestionId,
				opt => opt.MapFrom(src => src.Question.Id))
			.ForMember(dest => dest.Question,
				opt => opt.MapFrom(src => src.Question))
			.ReverseMap();

		CreateMap<TestResult, TestResultModel>()
			.ForMember(dest => dest.Id,
				opt => opt.MapFrom(src => src.Id))
			.ForMember(dest => dest.User,
				opt => opt.MapFrom(src => src.User))
			.ForMember(dest => dest.TestId,
				opt => opt.MapFrom(src => src.TestId))
			.ForMember(dest => dest.AnsweredQuestions,
				opt => opt.MapFrom(src => src.AnsweredQuestions))
			.ReverseMap();

		CreateMap<TestCompletionStatus, TestCompletionStatusModel>()
			.ReverseMap();
	}
}