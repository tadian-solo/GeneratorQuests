using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneretorQuests.ViewModels
{
    public interface IDialogManager

    {

        void Show(string key, object viewModel);

    }

    public static class DialogManagerExtensions

    {

        public static void Show<TViewModel>(this IDialogManager dialogManager, TViewModel viewModel)

        {

            var key = typeof(TViewModel).FullName;

            dialogManager.Show(key, viewModel);

        }

    }
}
