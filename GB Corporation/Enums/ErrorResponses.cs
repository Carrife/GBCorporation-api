namespace GB_Corporation.Enums
{
    public enum ErrorResponses
    {
        /// <summary>
        /// An existing record with the same login was already found.
        /// </summary>
        SameLoginExists = 1,

        /// <summary>
        /// An existing record with the same applicant was already found.
        /// </summary>
        HiringExists,

        /// <summary>
        /// Request data is not valid
        /// </summary>
        InvalidData,
    }
}
