using DAL.DbModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Mvc.Extensions.Mvc;
using Pepegov.UnitOfWork;
using Pepegov.UnitOfWork.Entityes;
using Pepegov.UnitOfWork.EntityFramework;
using Pepegov.UnitOfWork.EntityFramework.Repository;

namespace Mvc.Pages.MainPage;

public partial class Index
{
	
    private List<TestModel> _testList = new();
    private IRepositoryEntityFramework<TestModel> _testModelRepos;
    [Inject]
    private IUnitOfWorkManager _unitOfWorkManager { get; set; }
    private PageParameters _paginationParameters = new PageParameters();
    private IUnitOfWorkEntityFrameworkInstance _unitOfWork;
    private MetaData _metaData { get; set; } = new MetaData();
    private int _totalCount { get; set; }
    protected override async Task OnInitializedAsync()
    {
	    _unitOfWork = _unitOfWorkManager.GetInstance<IUnitOfWorkEntityFrameworkInstance>();
	    _testModelRepos = _unitOfWork.GetRepository<TestModel>();
	    await GetTests();
    }
    
    private async Task SelectedPage(int page)
    {
	    _paginationParameters.PageNumber = page;
	   
	    await GetTests();
    }
    private async Task GetTests()
    {
	    var tests = await _testModelRepos.GetPagedListAsync(
		    pageIndex: _paginationParameters.PageNumber - 1,
		    pageSize: _paginationParameters.PageSize,
		    disableTracking: true);
	    _totalCount = tests.TotalCount;

	    var pagedList = new Extensions.Mvc.PagedList<TestModel>(tests.Items.ToList(), _totalCount, _paginationParameters.PageNumber,
		    _paginationParameters.PageSize);
	    _testList = (List<TestModel>)tests.Items;
	    _metaData = pagedList.MetaData;
    }

    private async Task RowsPageChanged(int rows)
    {
	    _paginationParameters.PageSize = rows;
	    await GetTests();
    }
  
}