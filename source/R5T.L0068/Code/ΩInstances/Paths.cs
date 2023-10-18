using System;


namespace R5T.L0068
{
    public class Paths : IPaths
    {
        #region Infrastructure

        public static IPaths Instance { get; } = new Paths();


        private Paths()
        {
        }

        #endregion
    }
}
