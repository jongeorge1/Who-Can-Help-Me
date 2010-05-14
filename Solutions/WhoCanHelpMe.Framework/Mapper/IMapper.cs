namespace WhoCanHelpMe.Framework.Mapper
{
    public interface IMapper<TInput, TOutput>
    {
        TOutput MapFrom(TInput input);
    }

    public interface IMapper<TInput1, TInput2, TOutput>
    {
        TOutput MapFrom(TInput1 input1, TInput2 input2);
    }

    public interface IMapper<TInput1, TInput2, TInput3, TOutput>
    {
        TOutput MapFrom(TInput1 input1, TInput2 input2, TInput3 input3);
    }
}