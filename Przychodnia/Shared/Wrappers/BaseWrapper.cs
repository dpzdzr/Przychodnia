using CommunityToolkit.Mvvm.ComponentModel;

namespace Przychodnia.ViewModel.Base;

public abstract partial class BaseWrapper : ObservableValidator
{
    [ObservableProperty] protected int? id;

    protected static TWrapper? WrapIfNotNull<TSource, TWrapper>(
        TSource? source, Func<TSource, TWrapper> wrapperFactory)
        where TSource : class
        where TWrapper : class
            => source is not null ? wrapperFactory(source) : null;

    protected static List<TTarget>? MapListIfNotNull<TSource, TTarget>(
        IEnumerable<TSource> sourceList, Func<TSource, TTarget> mapper)
            => sourceList is not null ? [.. sourceList.Select(mapper)] : null;
}
