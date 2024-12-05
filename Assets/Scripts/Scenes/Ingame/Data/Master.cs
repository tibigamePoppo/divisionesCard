using System;

[Serializable]
public class DivisionData
{
    public int id;
    public string enName;
    public string name;
    public int surfaceSize;
    public int population;
    public float temperature;
    public int urban;
    public int village;
    public int forestSize;
    public int Hospitals;
    public int College;
}

[Serializable]
public class Master
{
    public DivisionData[] DivisionData;
}
