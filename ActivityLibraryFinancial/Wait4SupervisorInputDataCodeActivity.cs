using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;

namespace ActivityLibraryFinancial
{

    public sealed class Wait4SupervisorInputDataCodeActivity<T> : NativeActivity
    {
        // Define an activity input argument of type string
        public InArgument<string> BookMarkName { get; set; }
        public OutArgument<T> SupervisorResult { get; set; }

        protected override bool CanInduceIdle => true;

        // If your activity returns a value, derive from CodeActivity<TResult>
        // and return the value from the Execute method.

        protected override void Execute(NativeActivityContext context)
        {
            //Obtain the runtime value of the Text input argument
            string bookMarkName = context.GetValue(this.BookMarkName);

            context.CreateBookmark(bookMarkName, new BookmarkCallback(resumeCallBack));
        }

        private void resumeCallBack(NativeActivityContext context, Bookmark bookmark, object value)
        {
            context.SetValue(SupervisorResult, (T)value);
        }
    }
}
