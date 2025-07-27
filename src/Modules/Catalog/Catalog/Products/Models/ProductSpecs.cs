namespace Catalog.Products.Models;

public class ProductSpecs
{
    private Guid _categoryId;
    private string _searchProduct="";
    private const int MaxProductEntries = 5;
    private const int MaxPageSize = 20;
    public Guid CategoryId
    {
        get { return _categoryId; }
        set { _categoryId = value; }
    }

    public string SearchProduct
    {
        get { return _searchProduct; } 
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                _searchProduct = string.Empty;
            }
            else
            {

                _searchProduct = value;
            }
        }
    }

    private decimal _priceStart = default;
    private decimal _priceEnd = default;

    public decimal PriceStart
    {
        get { return _priceStart; }
        set
        {
            if(value < 0)
            {
                _priceStart = 0;
            }
            else
            {

                _priceStart = value;
            }
        }
    }


    public decimal PriceEnd
    {
        get { return _priceEnd; }
        set
        {
            if(value < 0 || value < _priceStart || value == _priceStart)
            {
                _priceEnd = _priceStart;
                PriceStart = 0;
            }
            else
            {
                _priceEnd = value;
            }
        }
    }

    public string? SortBy { get; set; }
    public bool isPaginationEnabled = false;
    public int PageSize => MaxProductEntries;
    private int _page = 1;
    public int Page
    {
        get { return _page; }
        set
        {
            if(value < 1)
            {
                _page = 1;
            }
            else
            {
                _page = value > MaxPageSize ? MaxPageSize : value;
            }

                
        }
    }
}
