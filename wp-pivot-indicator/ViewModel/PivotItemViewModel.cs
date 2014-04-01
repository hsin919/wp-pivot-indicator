namespace Japf.PivotIndicatorDemo.ViewModel
{
    public class PivotItemViewModel
    {
        private readonly string content;
        private readonly string title;

        public PivotItemViewModel(string content, string title)
        {
            this.content = content;
            this.title = title;
        }

        public string Content
        {
            get { return this.content; }
        }

        public string Title
        {
            get { return this.title; }
        }
    }
}