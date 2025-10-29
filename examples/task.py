import os
from typing import cast

import aind_behavior_services.task_logic.distributions as distributions
import numpy as np
from aind_behavior_curriculum import Stage, TrainerState
from scipy.linalg import expm

import aind_behavior_pavlovian_conditioning.task_logic as conditioning_task_logic
from aind_behavior_pavlovian_conditioning.task_logic import (
    AindPavlovianConitioningTaskLogic,
    AindPavlovianConditioningTaskParameters,
)

task_logic = AindPavlovianConitioningTaskLogic(
    stage_name="test_stage",
    task_parameters=AindPavlovianConditioningTaskParameters(
        rng_seed=0,
        environment=conditioning_task_logic.BlockStructure(
            blocks=[conditioning_task_logic.Block(name='test_block'), conditioning_task_logic.Block(name='test_block')]
        )
    )
)

def main(path_seed: str = "./local/{schema}.json"):
    example_task_logic = task_logic
    os.makedirs(os.path.dirname(path_seed), exist_ok=True)
    models = [example_task_logic]

    for model in models:
        with open(path_seed.format(schema=model.__class__.__name__), "w", encoding="utf-8") as f:
            f.write(model.model_dump_json(indent=2))


if __name__ == "__main__":
    main()