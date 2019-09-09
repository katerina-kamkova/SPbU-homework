namespace UniqueList
{
    /// <summary>
    /// The list that consists only unique elements
    /// </summary>
    public class UniqueList : List
    {
        /// <summary>
        /// Add value to the list on certain position only if there`s no such element yet
        /// </summary>
        /// <param name="value">Value to be added</param>
        /// <param name="position">The position of the added value</param>
        public override void Add(string value, int position)
        {
            if (CheckPresence(value))
            {
                throw new NotUniqueElementException();
            }
            base.Add(value, position);
        }
    }
}
