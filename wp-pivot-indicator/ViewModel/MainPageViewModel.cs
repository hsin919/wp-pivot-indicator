using System.Collections.ObjectModel;

namespace Japf.PivotIndicatorDemo.ViewModel
{
    public class MainPageViewModel : ViewModelBase
    {
        private readonly ObservableCollection<PivotItemViewModel> pivotItems;

        public MainPageViewModel()
        {
            this.pivotItems = new ObservableCollection<PivotItemViewModel>
                                  {
                                      new PivotItemViewModel("content 1", "item 1"),
                                      new PivotItemViewModel("content 2", "item 2"),
                                      new PivotItemViewModel("content 3", "item 3"),
                                      new PivotItemViewModel("content 4", "item 4"),
                                      new PivotItemViewModel("content 5", "item 5"),
                                  };
        }

        public ObservableCollection<PivotItemViewModel> PivotItems
        {
            get { return this.pivotItems; }
        }
    }
}
