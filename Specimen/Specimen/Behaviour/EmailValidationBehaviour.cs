using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Specimen
{
    public static class EmailValidationBehaviour
    {
        public static readonly BindableProperty AttachBehaviorProperty =
              BindableProperty.CreateAttached(
                  "AttachBehavior",
                  typeof(bool),
                  typeof(EmailValidationBehaviour),
                  false,
                  propertyChanged: OnAttachBehaviorChanged);

        public static bool GetAttachBehavior(BindableObject view)
        {
            return (bool)view.GetValue(AttachBehaviorProperty);
        }

        public static void SetAttachBehavior(BindableObject view, bool value)
        {
            view.SetValue(AttachBehaviorProperty, value);
        }

        static void OnAttachBehaviorChanged(BindableObject view, object oldValue, object newValue)
        {
            var entry = view as Entry;
            if (entry == null)
            {
                return;
            }

            bool attachBehavior = (bool)newValue;
            if (attachBehavior)
            {
                entry.TextChanged += OnEntryTextChanged;
            }
            else
            {
                entry.TextChanged -= OnEntryTextChanged;
            }
        }

        static void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {
            bool isValid = Regex.Match(args.NewTextValue, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").Success;
            if (isValid)
            {
                ((Entry)sender).BackgroundColor = Color.Default;

            }
           else
            {
                ((Entry)sender).BackgroundColor =  Color.FromHex("#f2a9a9");
            }
        }
    }
}
