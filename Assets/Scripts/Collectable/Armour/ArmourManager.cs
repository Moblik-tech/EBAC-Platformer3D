using System.Collections.Generic;
using Moblik.Core.Singleton;
using Moblik.Utils;

public class ArmourManager : Singleton<ArmourManager>
{
    public List<ArmourSetup> armourSetup;

    public ArmourSetup GetSetupByType(ArmourType armourType)
    {
        return armourSetup.Find(i => i.armourType == armourType);
    }
}