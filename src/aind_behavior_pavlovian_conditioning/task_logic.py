import logging
from enum import Enum
from typing import TYPE_CHECKING, Annotated, Any, Dict, List, Literal, Optional, Self, Union
import aind_behavior_services.task_logic.distributions as distributions

import aind_behavior_services.task_logic.distributions as distributions
from aind_behavior_services.task_logic import AindBehaviorTaskLogicModel, TaskParameters
from pydantic import BaseModel, Field, NonNegativeFloat, field_validator, model_validator
from typing_extensions import TypeAliasType

from aind_behavior_pavlovian_conditioning import (
    __semver__,
)

logger = logging.getLogger(__name__)

class Block(BaseModel):
    length: distributions.Distribution = Field(
        default = distributions.ExponentialDistribution(),
        description="The distribution from which the block length will be drawn from"
    )
    inter_trial_interval: distributions.Distribution = Field(
        default = distributions.ExponentialDistribution(),
        description="The distribution from which the inter trial interval length will be drawn from"
    ) # this could be in top-level environment
    name: str

class BlockStructure(BaseModel):
    blocks: List[Block]

class AindPavlovianConditioningTaskParameters(TaskParameters):
    environment: BlockStructure
    min_iti: float

class AindPavlovianConitioningTaskLogic(AindBehaviorTaskLogicModel):
    version: Literal[__semver__] = __semver__
    name: Literal["AindPavlovianConditioning"] = Field(default="AindPavlovianConditioning", description="Name of the task logic", frozen=True)
    task_parameters: AindPavlovianConditioningTaskParameters = Field(description="Parameters of the task logic")