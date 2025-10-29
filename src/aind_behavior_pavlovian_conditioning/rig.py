# Import core types
from __future__ import annotations

# Import core types
from typing import Literal, Optional

import aind_behavior_services.calibration.aind_manipulator as man
import aind_behavior_services.calibration.olfactometer as oc
import aind_behavior_services.calibration.treadmill as treadmill
import aind_behavior_services.calibration.water_valve as wvc
import aind_behavior_services.rig as rig
from pydantic import BaseModel, Field

# from aind_behavior_pavlovian_conditioning import __semver__

class AindPavlovianConditioningRig(rig.AindBehaviorRigModel):
    # version: Literal[__semver__] = __semver__
    harp_behavior: rig.harp.HarpBehavior = Field(..., description="Harp behavior")
    harp_sound_card: rig.harp.HarpSoundCard = Field(..., description="Harp sound card")