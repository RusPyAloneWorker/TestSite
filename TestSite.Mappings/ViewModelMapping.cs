using AutoMapper;
using TestSite.Contracts.ViewInputModels;
using TestSite.Contracts.ViewModels;
using TestSite.Domain;
using TestSite.Domain.TestResultRoot;
using TestSite.Domain.TestRoot;
using TestSite.Infrastructure.DatabaseModels;

namespace TestSite.Mappings;

public class ViewModelMapping: Profile
{
	public ViewModelMapping()
	{
		CreateMap<QuestionVM, Question>()
			.ForMember(dest => dest.HasMultipleAnswers,
				opt => opt.MapFrom(src => src.HasMultipleAnswers))
			.ForMember(dest => dest.Text,
				opt => opt.MapFrom(src => src.Text));

		CreateMap<TestVM, Test>()
			.ForMember(dest => dest.Description,
				opt => opt.MapFrom(src => src.Description))
			.ForMember(dest => dest.TimeSpan,
				opt => opt.MapFrom(src => new TimeSpan(0, src.TimeSpan, 0)))
			.ForMember(dest => dest.Title,
				opt => opt.MapFrom(src => src.Title));

		CreateMap<UserVM, User>()
			.ForMember(dest => dest.Id,
				opt => opt.MapFrom(src => src.Id));

		CreateMap<QuestionOption, QuestionOptionVm>()
			.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
			.ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Text))
			.ForMember(dest => dest.IsCorrect, opt => opt.MapFrom(src => src.IsCorrect))
			.ReverseMap();
		
		CreateMap<Question, QuestionVm>()
			.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
			.ForMember(dest => dest.HasMultipleAnswers, opt => opt.MapFrom(src => src.HasMultipleAnswers))
			.ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Text))
			.ForMember(dest => dest.QuestionOptions, opt => opt.MapFrom(src => src.Answers))
			.ReverseMap();
		
		CreateMap<Test, TestVm>()
			.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
			.ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
			.ForMember(dest => dest.TimeSpan, opt => opt.MapFrom(src => src.TimeSpan))
			.ForMember(dest => dest.Questions, opt => opt.MapFrom(src => src.Questions));

		CreateMap<AnsweredQuestion, AnsweredQuestionVM>()
			.ForMember(dest => dest.QuestionOptions, 
				opt => opt.MapFrom(src => src.QuestionOptions))
			.ForMember(dest => dest.Question, 
				opt => opt.MapFrom(src => src.Question))
			.ReverseMap();

		CreateMap<TestResult, TestResultVM>()
			.ForMember(dest => dest.AnsweredQuestions,
				opt => opt.MapFrom(src => src.AnsweredQuestions))
			.ForMember(dest => dest.TestId,
				opt => opt.MapFrom(src => src.TestId));
	}
}