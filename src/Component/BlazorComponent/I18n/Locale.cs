
namespace BlazorComponent.Components
{
    public class Locale
    {
        public string Badge { get; set; }
        public string Close { get; set; }
        public Dataiterator DataIterator { get; set; }
        public Datatable DataTable { get; set; }
        public Datafooter DataFooter { get; set; }
        public Datepicker DatePicker { get; set; }
        public string NoDataText { get; set; }
        public Carousel Carousel { get; set; }
        public Calendar Calendar { get; set; }
        public Fileinput FileInput { get; set; }
        public Timepicker TimePicker { get; set; }
        public Pagination Pagination { get; set; }
        public Rating Rating { get; set; }
    }

    public class Dataiterator
    {
        public string NoResultsText { get; set; }
        public string LoadingText { get; set; }
    }

    public class Datatable
    {
        public string ItemsPerPageText { get; set; }
        public Arialabel AriaLabel { get; set; }
        public string SortBy { get; set; }
    }

    public class Arialabel
    {
        public string SortDescending { get; set; }
        public string SortAscending { get; set; }
        public string SortNone { get; set; }
        public string ActivateNone { get; set; }
        public string ActivateDescending { get; set; }
        public string ActivateAscending { get; set; }
    }

    public class Datafooter
    {
        public string ItemsPerPageText { get; set; }
        public string ItemsPerPageAll { get; set; }
        public string NextPage { get; set; }
        public string PrevPage { get; set; }
        public string FirstPage { get; set; }
        public string LastPage { get; set; }
        public string PageText { get; set; }
    }

    public class Datepicker
    {
        public string ItemsSelected { get; set; }
        public string NextMonthAriaLabel { get; set; }
        public string NextYearAriaLabel { get; set; }
        public string PrevMonthAriaLabel { get; set; }
        public string PrevYearAriaLabel { get; set; }
    }

    public class Carousel
    {
        public string Prev { get; set; }
        public string Next { get; set; }
        public CarouselArialabel AriaLabel { get; set; }
    }

    public class CarouselArialabel
    {
        public string Delimiter { get; set; }
    }

    public class Calendar
    {
        public string MoreEvents { get; set; }
    }

    public class Fileinput
    {
        public string Counter { get; set; }
        public string CounterSize { get; set; }
    }

    public class Timepicker
    {
        public string Am { get; set; }
        public string Pm { get; set; }
    }

    public class Pagination
    {
        public PaginationArialabel AriaLabel { get; set; }
    }

    public class PaginationArialabel
    {
        public string Wrapper { get; set; }
        public string Next { get; set; }
        public string Previous { get; set; }
        public string Page { get; set; }
        public string CurrentPage { get; set; }
    }

    public class Rating
    {
        public RatingArialabel AriaLabel { get; set; }
    }

    public class RatingArialabel
    {
        public string Icon { get; set; }
    }

}