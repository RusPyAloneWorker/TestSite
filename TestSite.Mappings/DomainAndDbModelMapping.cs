using AutoMapper;
using TestSite.Domain.TestRoot;
using TestSite.Infrastructure.DatabaseModels;
using Test = TestSite.Domain.TestRoot.Test;

namespace TestSite.Mappings;

public class DomainAndDbModelMapping: Profile
{
	public DomainAndDbModelMapping()
	{
		CreateMap<Test, TestModel>().ReverseMap();
		CreateMap<Question, QuestionModel>().ReverseMap();
		CreateMap<QuestionOption, QuestionOptionModel>().ReverseMap();
	}
}