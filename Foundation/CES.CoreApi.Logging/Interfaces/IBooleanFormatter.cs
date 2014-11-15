namespace CES.CoreApi.Logging.Interfaces
{
    public interface IBooleanFormatter
    {
        /// <summary>
        /// Formats input string as Yes/No
        /// </summary>
        string Format(string inputValue);

        /// <summary>
        /// Formats input string as Yes/No
        /// </summary>
        string Format(int inputValue);

        /// <summary>
        /// Formats input string as Yes/No string
        /// </summary>
        string Format(bool inputValue);
    }
}