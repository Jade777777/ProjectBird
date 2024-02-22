namespace Core.Managers.Analytics
{
    public struct Parameter
    {
        public string ParameterName { get; private set; }
        public string ParameterValue { get; private set; }

        public Parameter(string parameterName, string parameterValue)
        {
            ParameterName = parameterName;
            ParameterValue = parameterValue;
        }
    }
}