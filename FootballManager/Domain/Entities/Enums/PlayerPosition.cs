using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entities.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum PlayerPosition
    {
        GK, CB, LB, RB, LWB, RWB, CM, CAM, LM, RM, CF, ST, LW, RW
    }
}
