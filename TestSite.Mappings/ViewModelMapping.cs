using AutoMapper;
using TestSite.Contracts.ViewModels;
using TestSite.Domain.TestRoot;

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
		
	}
}