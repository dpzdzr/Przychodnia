using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przychodnia.ViewModel.Wrapper;

public static class WrapperHelper
{
    public static TWrapper? WrapPropertyIfNotNull<TSource, TWrapper>
        (TSource? source, Func<TSource, TWrapper> wrapperFactory)
        where TSource : class
        where TWrapper : class
        => source is not null ? wrapperFactory(source) : null;
    
}
