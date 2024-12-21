using Bookstore_App.Domain;
using Bookstore_App.Presentation.Commands;
using Bookstore_App.Services;
using System.Collections;
using System.Collections.ObjectModel;

namespace Bookstore_App.Presentation.ViewModel
{
    internal class CatalogViewModel : ViewModelBase
    {
        private bool _isCatalogMode = false;
        public bool IsCatalogMode
        {
            get => _isCatalogMode;
            set
            {
                _isCatalogMode = value;
                RaisePropertyChanged();
                OpenAddBookCommand.RaiseCanExecuteChanged();
                RemoveAuthorCommand.RaiseCanExecuteChanged();
                OpenEditAuthorsCommand.RaiseCanExecuteChanged();
            }
        }
        private bool _isSaving = false;
        public bool IsSaving
        {
            get => _isSaving;
            set
            {
                _isSaving = value;
                RaisePropertyChanged();
            }
        }

        private bool _isEditingBook = false;
        public bool IsEditingBook
        {
            get => _isEditingBook;
            set
            {
                _isEditingBook = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<BookViewModel> _books = new();
        public ObservableCollection<BookViewModel> Books
        {
            get => _books;
            set
            {
                _books = value;
                RaisePropertyChanged();
            }
        }

        private BookViewModel _activeBook;
        public BookViewModel ActiveBook
        {
            get => _activeBook;
            set
            {
                _activeBook = value;
                RaisePropertyChanged();
            }
        }

        private IList _selectedBooks = new ArrayList();

        public IList SelectedBooks

        {
            get => _selectedBooks;
            set
            {
                _selectedBooks = value;
                RaisePropertyChanged();
                RemoveBookCommand.RaiseCanExecuteChanged();
                OpenEditBookCommand.RaiseCanExecuteChanged();
            }
        }

        private List<BookViewModel> booksToRemoveInDatabase = new();
        private List<BookViewModel> booksToSaveInDatabase = new();
        private List<BookViewModel> booksToEditInDatabase = new();

        private string _title;
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                RaisePropertyChanged();
            }
        }
        private string _ISBN13;
        public string ISBN13
        {
            get => _ISBN13;
            set
            {
                _ISBN13 = value;
                RaisePropertyChanged();
            }
        }
        private string _price;
        public string Price
        {
            get => _price;
            set
            {
                _price = value;
                RaisePropertyChanged();
            }
        }
        private string _year;
        public string Year
        {
            get => _year;
            set
            {
                _year = value;
                RaisePropertyChanged();
            }
        }
        private string _month;
        public string Month
        {
            get => _month;
            set
            {
                _month = value;
                RaisePropertyChanged();
            }
        }
        private string _day;
        public string Day
        {
            get => _day;
            set
            {
                _day = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<Language> _languages;
        public ObservableCollection<Language> Languages
        {
            get => _languages;
            set
            {
                _languages = value;
                RaisePropertyChanged();
            }
        }

        private Language _selectedLanguage;
        public Language SelectedLanguage
        {
            get => _selectedLanguage;
            set
            {
                _selectedLanguage = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<Author> _authors;
        public ObservableCollection<Author> Authors
        {
            get => _authors;
            set
            {
                _authors = value;
                RaisePropertyChanged();
            }
        }

        private Author _selectedAuthor;
        public Author SelectedAuthor
        {
            get => _selectedAuthor;
            set
            {
                _selectedAuthor = value;
                RaisePropertyChanged();
                OpenEditAuthorCommand.RaiseCanExecuteChanged();
                RemoveAuthorCommand.RaiseCanExecuteChanged();
            }
        }

        private Author _selectedAuthorToAddToBook;
        public Author SelectedAuthorToAddToBook
        {
            get => _selectedAuthorToAddToBook;
            set
            {
                _selectedAuthorToAddToBook = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<Author> _selectedAuthors = new();
        public ObservableCollection<Author> SelectedAuthors
        {
            get => _selectedAuthors;
            set
            {
                _selectedAuthors = value;
                RaisePropertyChanged();
            }
        }

        private string _authorFirstName;

        public string AuthorFirstName
        {
            get => _authorFirstName;
            set
            {
                _authorFirstName = value;
                RaisePropertyChanged();
            }
        }

        private string _authorLastName;

        public string AuthorLastName
        {
            get => _authorLastName;
            set
            {
                _authorLastName = value;
                RaisePropertyChanged();
            }
        }
        private string _authorYear;
        public string AuthorYear
        {
            get => _authorYear;
            set
            {
                _authorYear = value;
                RaisePropertyChanged();
            }
        }
        private string _authorMonth;
        public string AuthorMonth
        {
            get => _authorMonth;
            set
            {
                _authorMonth = value;
                RaisePropertyChanged();
            }
        }
        private string _authorDay;
        public string AuthorDay
        {
            get => _authorDay;
            set
            {
                _authorDay = value;
                RaisePropertyChanged();
            }
        }


        private ObservableCollection<SubCategory> _subCategories;
        public ObservableCollection<SubCategory> SubCategories
        {
            get => _subCategories;
            set
            {
                _subCategories = value;
                RaisePropertyChanged();
            }
        }

        private SubCategory _selectedSubCategory;
        public SubCategory SelectedSubCategory
        {
            get => _selectedSubCategory;
            set
            {
                _selectedSubCategory = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<PrimaryAudience> _primaryAudiences;
        public ObservableCollection<PrimaryAudience> PrimaryAudiences
        {
            get => _primaryAudiences;
            set
            {
                _primaryAudiences = value;
                RaisePropertyChanged();
            }
        }

        private PrimaryAudience _selectedPrimaryAudience;
        public PrimaryAudience SelectedPrimaryAudience
        {
            get => _selectedPrimaryAudience;
            set
            {
                _selectedPrimaryAudience = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<PublishingHouse> _publishingHouses;
        public ObservableCollection<PublishingHouse> PublishingHouses
        {
            get => _publishingHouses;
            set
            {
                _publishingHouses = value;
                RaisePropertyChanged();
            }
        }

        private PublishingHouse _selectedPublishingHouse;
        public PublishingHouse SelectedPublishingHouse
        {
            get => _selectedPublishingHouse;
            set
            {
                _selectedPublishingHouse = value;
                RaisePropertyChanged();
            }
        }
        public Action? OpenAddBook { get; set; }
        public DelegateCommand OpenAddBookCommand { get; set; }

        public Action<String, String>? ShowError { get; set; }

        public DelegateCommand AddAuthorToBookCommand { get; set; }

        public DelegateCommand RemoveBookCommand { get; set; }

        public Action? OpenAddAuthor { get; set; }
        public DelegateCommand OpenAddAuthorCommand { get; set; }

        public Action? OpenEditAuthors { get; set; }
        public DelegateCommand OpenEditAuthorsCommand { get; set; }

        public DelegateCommand RemoveAuthorCommand { get; set; }

        public Action? OpenEditAuthor { get; set; }
        public DelegateCommand OpenEditAuthorCommand { get; set; }

        public Action? OpenEditBook { get; set; }
        public DelegateCommand OpenEditBookCommand { get; set; }

        public CatalogViewModel()
        {
            OpenAddBookCommand = new DelegateCommand(DoOpenAddBook, CanOpenAddBook);
            AddAuthorToBookCommand = new DelegateCommand(AddAuthorToBook, CanAddAuthorToBook);
            OpenAddAuthorCommand = new DelegateCommand(DoOpenAddAuthor, CanOpenAddAuthor);
            RemoveBookCommand = new DelegateCommand(RemoveBookOrBooks, CanRemoveBook);
            OpenEditAuthorsCommand = new DelegateCommand(DoOpenEditAuthors, CanOpenEditAuthors);
            RemoveAuthorCommand = new DelegateCommand(RemoveAuthor, CanRemoveAuthor);
            OpenEditAuthorCommand = new DelegateCommand(DoOpenEditAuthor, CanOpenEditAuthor);
            OpenEditBookCommand = new DelegateCommand(DoOpenEditBook, CanOpenEditBook);

        }

        private void DoOpenEditBook(object obj)
        {
            IsEditingBook = true;

            ActiveBook = (BookViewModel)SelectedBooks[0];
            SelectedAuthor = ActiveBook.Authors.FirstOrDefault();
            SelectedAuthors = ActiveBook.Authors;
            SelectedLanguage = ActiveBook.Language;
            SelectedPrimaryAudience = ActiveBook.PrimaryAudience;
            SelectedPublishingHouse = ActiveBook.PublishingHouse;

            SelectedSubCategory = ActiveBook.SubCategories.ToList().FirstOrDefault();
            var listOfDateParts = new string[3];
            listOfDateParts = ActiveBook.ReleaseDate.ToString().Split('-');
            Year = listOfDateParts[0];
            Month = listOfDateParts[1];
            Day = listOfDateParts[2];

            OpenEditBook?.Invoke();
        }

        private bool CanOpenEditBook(object? arg) => IsCatalogMode && SelectedBooks.Count == 1;

        public void UpdateBook()
        {
            var bookToEdit = ActiveBook.GetModel();
            ActiveBook.LanguageId = SelectedLanguage.Id;
            ActiveBook.Language = SelectedLanguage;
            ActiveBook.PrimaryAudienceId = SelectedPrimaryAudience.Id;
            ActiveBook.PrimaryAudience = SelectedPrimaryAudience;
            ActiveBook.PublishingHouseId = SelectedPublishingHouse.Id;
            ActiveBook.PublishingHouse = SelectedPublishingHouse;
            //TODO: Fix adding multiple subcategories

            ActiveBook.ReleaseDate = new DateOnly(Int32.Parse(Year), Int32.Parse(Month), Int32.Parse(Day));


            ActiveBook.SubCategories.Clear();
            ActiveBook.SubCategories.Add(SelectedSubCategory);

            DataManager.AddNewSubcategoryToBook(bookToEdit, SelectedSubCategory);

            DataManager.UpdateBook(bookToEdit);

        }
        public void StopEditing()
        {
            IsEditingBook = false;
        }
        private void DoOpenEditAuthor(object obj)
        {
            AuthorFirstName = SelectedAuthor.FirstName;
            AuthorLastName = SelectedAuthor.LastName;
            var listOfDateParts = new string[3];
            listOfDateParts = SelectedAuthor.DateOfBirth.ToString().Split('-');
            AuthorYear = listOfDateParts[0];
            AuthorMonth = listOfDateParts[1];
            AuthorDay = listOfDateParts[2];

            OpenEditAuthor?.Invoke();
        }

        private bool CanOpenEditAuthor(object? arg) => SelectedAuthor is not null;

        //TODO: Fix async
        public async Task UpdateAuthor()
        {
            SelectedAuthor.FirstName = AuthorFirstName;
            SelectedAuthor.LastName = AuthorLastName;
            SelectedAuthor.DateOfBirth = new DateOnly(Int32.Parse(AuthorYear), Int32.Parse(AuthorMonth), Int32.Parse(AuthorDay));
            DataManager.UpdateAuthor(SelectedAuthor);

            //TODO: NEEDED?
            await GetAndSetBooksForCatalogView();
            SelectedAuthor = Authors.FirstOrDefault();

        }

        private void RemoveAuthor(object obj)
        {
            DataManager.RemoveAuthor(SelectedAuthor);

            GetAndSetAuthors();
        }

        private bool CanRemoveAuthor(object? arg) => IsCatalogMode && SelectedAuthor is not null;

        private void DoOpenEditAuthors(object obj)
        {
            OpenEditAuthors?.Invoke();
        }

        private bool CanOpenEditAuthors(object? arg) => IsCatalogMode && !IsSaving;

        //TODO: FIX! //Isediting?
        private void AddAuthorToBook(object obj)
        {
            if (!ActiveBook.Authors.Any(a => a.Id == SelectedAuthorToAddToBook.Id))
            {
                ActiveBook.Authors.Add(SelectedAuthorToAddToBook);
                if (IsEditingBook)
                {
                    var book = ActiveBook.GetModel();
                    DataManager.AddAuthorToBook(SelectedAuthorToAddToBook, book);
                }
                SelectedAuthor = SelectedAuthorToAddToBook;
                ActiveBook.UpdateLastNames();
            }
        }

        private bool CanAddAuthorToBook(object? arg) => true;

        private void DoOpenAddAuthor(object obj) => OpenAddAuthor?.Invoke();

        private bool CanOpenAddAuthor(object? arg) => IsCatalogMode;

        private void DoOpenAddBook(object obj)
        {
            SetNewActiveBookAndResetInput();
            OpenAddBook?.Invoke();
        }
        private void SetNewActiveBookAndResetInput()
        {
            SelectedAuthorToAddToBook = Authors.FirstOrDefault();
            SelectedPrimaryAudience = PrimaryAudiences.FirstOrDefault();
            SelectedPublishingHouse = PublishingHouses.FirstOrDefault();
            SelectedLanguage = Languages.FirstOrDefault();
            SelectedSubCategory = SubCategories.FirstOrDefault();
            Year = "0001";
            Month = "01";
            Day = "01";
            ActiveBook = new BookViewModel(new Book
            {
                Isbn13 = "9780000000000",
                Title = "Enter title here",
                LanguageId = SelectedLanguage.Id,
                Language = SelectedLanguage,
                PrimaryAudienceId = SelectedPrimaryAudience.Id,
                PrimaryAudience = SelectedPrimaryAudience,
                Price = 199.00M,
                ReleaseDate = new DateOnly(Int32.Parse(Year), Int32.Parse(Month), Int32.Parse(Day)),
                PublishingHouseId = SelectedPublishingHouse.Id,
                PublishingHouse = SelectedPublishingHouse
            });

            //SelectedAuthor = ActiveBook.Authors.FirstOrDefault();


            //SelectedAuthors.Clear();
        }

        private bool CanOpenAddBook(object? arg) => IsCatalogMode && !IsSaving;
        public async Task GetAndSetBooksForCatalogView()
        {
            var cataloginfo = await DataManager.GetCatalogInfoAsync();

            Books = cataloginfo.Books;
            Languages = cataloginfo.Languages;
            Authors = cataloginfo.Authors;
            SubCategories = cataloginfo.SubCategories;
            PrimaryAudiences = cataloginfo.PrimaryAudiences;
            PublishingHouses = cataloginfo.PublishingHouses;

            SetNewActiveBookAndResetInput();
        }

        public bool GetBookInputIsCorrect()
        {
            if (String.IsNullOrEmpty(ActiveBook.Title))
            {
                ShowError?.Invoke("Missing title!", "Wrong input!");

            }
            else if (String.IsNullOrEmpty(ActiveBook.Isbn13))
            {
                ShowError?.Invoke("Please enter ISBN13", "Wrong input!");

            }
            else if (!(ActiveBook.Isbn13.Length == 13
                && Int64.TryParse(ActiveBook.Isbn13, out long isbn13)
                && ActiveBook.Isbn13.StartsWith("978")
                || ActiveBook.Isbn13.StartsWith("979")))
            {
                ShowError?.Invoke("ISBN13 is not correctly input", "Wrong input!");
            }
            //TODO: IF is not editing
            else if (Books.Any(b => b.Isbn13 == ActiveBook.Isbn13) && !IsEditingBook)
            {
                ShowError?.Invoke("Can't insert duplicate ISBN13", "Wrong input!");
            }
            else if (String.IsNullOrEmpty(Year))
            {
                ShowError?.Invoke("Please enter a year", "Wrong input!");
            }
            else if (!Int32.TryParse(Year, out int year) || !(year > 0) || !(year <= 9999))
            {
                ShowError?.Invoke("Please enter year correctly (1 - 9999)", "Wrong input!");

            }
            else if (String.IsNullOrEmpty(Month))
            {
                ShowError?.Invoke("Please enter a Month", "Wrong input!");

            }
            else if (!Int32.TryParse(Month, out int month) || !(month > 0) || !(month <= 12))
            {
                ShowError?.Invoke("Please enter month correctly (1 - 12)", "Wrong input!");

            }
            else if (String.IsNullOrEmpty(Day))
            {
                ShowError?.Invoke("Please enter a day", "Wrong input!");

            }
            else if (!Int32.TryParse(Day, out int day) || !(day > 0) || !(day <= 31))
            {
                ShowError?.Invoke("Please enter day correctly (1 - 31)", "Wrong input!");

            }
            else if (String.IsNullOrEmpty(ActiveBook.Price.ToString()) || !(ActiveBook.Price > 0))
            {
                ShowError?.Invoke("Please enter price correctly (comma separated positive number)", "Wrong input!");
            }
            else if (SelectedLanguage is null)
            {
                ShowError?.Invoke("Please select a language", "Wrong input!");
            }
            else if (ActiveBook.Authors.Count == 0)
            {
                ShowError?.Invoke("Please select author(s)", "Wrong input!");
            }
            else if (SelectedSubCategory is null)
            {
                ShowError?.Invoke("Please select a sub category", "Wrong input!");
            }
            else if (SelectedPrimaryAudience is null)
            {
                ShowError?.Invoke("Please select a primary audience", "Wrong input!");
            }
            else if (SelectedPublishingHouse is null)
            {
                ShowError?.Invoke("Please select a publishing house", "Wrong input!");
            }
            else
            {
                return true;
            }
            return false;
        }

        internal bool GetAuthorInputIsCorrect()
        {
            if (String.IsNullOrEmpty(AuthorFirstName))
            {
                ShowError?.Invoke("Please enter first name!", "Wrong input!");

            }
            else if (String.IsNullOrEmpty(AuthorLastName))
            {
                ShowError?.Invoke("Please enter last name!", "Wrong input!");

            }
            else if (String.IsNullOrEmpty(AuthorYear))
            {
                ShowError?.Invoke("Please enter a year", "Wrong input!");
            }
            else if (!Int32.TryParse(AuthorYear, out int year) || !(year > 0) || !(year <= 9999))
            {
                ShowError?.Invoke("Please enter year correctly (1 - 9999)", "Wrong input!");

            }
            else if (String.IsNullOrEmpty(AuthorMonth))
            {
                ShowError?.Invoke("Please enter a Month", "Wrong input!");

            }
            else if (!Int32.TryParse(AuthorMonth, out int month) || !(month > 0) || !(month <= 12))
            {
                ShowError?.Invoke("Please enter month correctly (1 - 12)", "Wrong input!");

            }
            else if (String.IsNullOrEmpty(AuthorDay))
            {
                ShowError?.Invoke("Please enter a day", "Wrong input!");

            }
            else if (!Int32.TryParse(AuthorDay, out int day) || !(day > 0) || !(day <= 31))
            {
                ShowError?.Invoke("Please enter day correctly (1 - 31)", "Wrong input!");

            }
            else if (Authors.Any(
                a => a.FirstName == AuthorFirstName
                && a.LastName == AuthorLastName
                && a.DateOfBirth == new DateOnly(Int32.Parse(AuthorYear), Int32.Parse(AuthorMonth), Int32.Parse(AuthorDay))))
            {
                ShowError?.Invoke("Can't have duplicate authors", "Wrong input!");
            }
            else
            {
                return true;
            }
            return false;
        }

        public void AddNewAuthorToDatabase()
        {
            DataManager.InsertAuthor(AuthorFirstName, AuthorLastName, new DateOnly(Int32.Parse(AuthorYear), Int32.Parse(AuthorMonth), Int32.Parse(AuthorDay)));

            GetAndSetAuthors();
        }

        public void GetAndSetAuthors()
        {
            Authors = DataManager.GetAuthors();
            SelectedAuthor = Authors.FirstOrDefault();
        }

        //TODO: Clean up, use getmodel instead?
        public void AddBook()
        {

            ActiveBook.LanguageId = SelectedLanguage.Id;
            ActiveBook.Language = SelectedLanguage;
            ActiveBook.PrimaryAudienceId = SelectedPrimaryAudience.Id;
            ActiveBook.PrimaryAudience = SelectedPrimaryAudience;
            ActiveBook.ReleaseDate = new DateOnly(Int32.Parse(Year), Int32.Parse(Month), Int32.Parse(Day));
            ActiveBook.PublishingHouseId = SelectedPublishingHouse.Id;
            ActiveBook.PublishingHouse = SelectedPublishingHouse;
            ActiveBook.SubCategories.Clear();
            ActiveBook.SubCategories.Add(SelectedSubCategory);
            Books.Add(ActiveBook);

            DataManager.InsertBook(new Book()
            {
                Isbn13 = ActiveBook.Isbn13,
                Title = ActiveBook.Title,
                LanguageId = ActiveBook.LanguageId,
                PrimaryAudienceId = ActiveBook.PrimaryAudienceId,
                Price = ActiveBook.Price,
                ReleaseDate = ActiveBook.ReleaseDate,
                PublishingHouseId = ActiveBook.PublishingHouseId,
                InventoryBalances = ActiveBook.InventoryBalances,
                Authors = ActiveBook.Authors,
                SubCategories = ActiveBook.SubCategories
            });
        }

        private void RemoveBookOrBooks(object obj)
        {
            var booksToRemove = new List<BookViewModel>();
            var booksToRemoveFromDatabase = new List<Book>();

            foreach (var book in SelectedBooks)
            {
                booksToRemove.Add((BookViewModel)book);

                var bookModel = ((BookViewModel)book).GetModel();
                bookModel.Language = null;
                bookModel.PublishingHouse = null;
                bookModel.PrimaryAudience = null;
                booksToRemoveFromDatabase.Add(bookModel);
            }

            DataManager.RemoveBookOrBooks(booksToRemoveFromDatabase);

            for (int i = 0; i < booksToRemove.Count; i++)
            {
                Books.Remove(booksToRemove[i]);
            }

        }

        private bool CanRemoveBook(object? arg) => IsCatalogMode && SelectedBooks is not null;
    }
}
