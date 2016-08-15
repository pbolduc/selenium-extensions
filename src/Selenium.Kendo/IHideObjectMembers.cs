namespace Selenium.Kendo
{
    using System;
    using System.ComponentModel;

    /// <summary>
    /// Interface that is used to hidee methods declared by <see cref="object"/> from IntelliSense.
    /// </summary>
    /// <remarks>
    /// Code that consumes implementations of this interface should expect one of two things:
    /// <list type = "number">
    ///   <item>When referencing the interface from within the same solution (project reference), you will still see the methods this interface is meant to hide.</item>
    ///   <item>When referencing the interface through the compiled output assembly (external reference), the standard Object methods will be hidden as intended.</item>
    /// </list>
    /// See http://bit.ly/ifluentinterface for more information.
    /// </remarks>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IHideObjectMembers
    {
        /// <summary>
        /// Redeclaration that hides the <see cref="object.GetType()"/> method from IntelliSense.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        Type GetType();

        /// <summary>
        /// Redeclaration that hides the <see cref="object.GetHashCode()"/> method from IntelliSense.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        int GetHashCode();

        /// <summary>
        /// Redeclaration that hides the <see cref="object.ToString()"/> method from IntelliSense.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        string ToString();

        /// <summary>
        /// Redeclaration that hides the <see cref="object.Equals(object)"/> method from IntelliSense.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        bool Equals(object obj);
    }

    public interface IKendoWidgetApi : IHideObjectMembers
    {        
    }

    public abstract class KendoWidgetApi<TWidget> : IKendoWidgetApi
    {
        protected TWidget Widget { get; }

        protected KendoWidgetApi(TWidget widget)
        {
            if (widget == null)
            {
                throw new ArgumentNullException(nameof(widget));
            }

            Widget = widget;
        }
    }
}