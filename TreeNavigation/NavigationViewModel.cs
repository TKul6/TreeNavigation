using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ReactiveUI;

namespace TreeNavigation
{
    /// <summary>
    /// Navigation logic for WPF TreeView
    /// </summary>
    public class NavigationViewModel : ReactiveObject
    {

        /// <summary>
        /// The input stream from KeyUp event
        /// </summary>
        private IObservable<string> mLettersStream;


        /// <summary>
        /// The phrase to navigate to
        /// </summary>
        private ObservableAsPropertyHelper<string> mNavigationPhrase;

        /// <summary>
        /// Gets the <see cref="mNavigationPhrase"/>
        /// </summary>
        public string NavigationPhrase
        {
            get { return mNavigationPhrase.Value; }
        }

        public NavigationViewModel(UIElement operationElement)
        {

            var event1 =
                     Observable.FromEventPattern<KeyEventArgs>(operationElement, "KeyUp");

            

            mLettersStream = event1.Select(
                arg => arg.EventArgs.Key.ToString().ToLower());

            mLettersStream.Scan(String.Concat).ToProperty(this, vm => vm.NavigationPhrase).ObserveOn(SynchronizationContext.Current).Subscribe(_ => MessageBox.Show(NavigationPhrase));


            

        }

    }
}
