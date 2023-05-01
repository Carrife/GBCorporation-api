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

        /// <summary>
        /// An existing record with the same data was already found.
        /// </summary>
        SameDataExists,

        /// <summary>
        /// Email is not valid
        /// </summary>
        InvalidEmail,

        /// <summary>
        /// Password is not valid
        /// </summary>
        InvalidPassword,
    }
}
