using AutoMapper;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TestSite.Contracts.ViewModels;
using TestSite.Domain.Abstractions;
using TestSite.Domain.TestRoot;

namespace TestSite.Pages;

public class ShowTestsModel : PageModel
{
	private readonly ITestRepository _testRepository;
	private readonly IMapper _mapper;
	
	public List<TestVm> Tests { get; set; }

	public ShowTestsModel(
		ITestRepository testRepository,
		IMapper mapper)
	{
		_testRepository = testRepository;
		_mapper = mapper;
	}
	
	public async Task OnGet()
	{
		var testModels = await _testRepository.GetTestsAsync();

		Tests = _mapper.Map<List<Test>, List<TestVm>>(testModels.Value!);
	}
}