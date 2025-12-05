import datetime
import os

import aind_behavior_services.rig as rig
from aind_behavior_pavlovian_conditioning.rig import (
    AindPavlovianConditioningRig
)
from aind_behavior_services.rig.harp import (
    HarpBehavior,
    HarpSoundCard
)

video_writer = rig.cameras.VideoWriterFfmpeg(frame_rate=60, container_extension="mp4")

rig = AindPavlovianConditioningRig(
    rig_name="test_rig",
    harp_behavior=HarpBehavior(port_name="COM6"),
    harp_sound_card=HarpSoundCard(port_name="COM11"),
    triggered_camera_controller=rig.cameras.CameraController[rig.cameras.SpinnakerCamera](
        frame_rate=60,
        cameras={
            "FaceCamera": rig.cameras.SpinnakerCamera(
                serial_number="23022715", binning=1, exposure=5000, gain=0, video_writer=video_writer
            ),
            "BodyCamera": rig.cameras.SpinnakerCamera(
                serial_number="22511925", binning=1, exposure=5000, gain=0, video_writer=video_writer
            )
        }
    )
)

def main(path_seed: str = "./local/{schema}.json"):
    os.makedirs(os.path.dirname(path_seed), exist_ok=True)
    models = [rig]

    for model in models:
        with open(path_seed.format(schema=model.__class__.__name__), "w", encoding="utf-8") as f:
            f.write(model.model_dump_json(indent=2))


if __name__ == "__main__":
    main()