[![License](https://img.shields.io/badge/license-MIT-brightgreen)](LICENSE)
## Pavlovian conditioning behavioral paradigm overview
This Bonsai code is for pavlovian conditioning for head-fixed mice using auditory tones as conditioned stimuli (CSs) and both positive liquid reward and aversive airpuff stimuli as unconditioned stimuli (USs).

<img width="273" alt="image" src="https://github.com/user-attachments/assets/db41c337-b11d-43b7-8dd6-e0ca9a44a659" />



Each trial starts with a CS with 1s duration, followed by a 1s delay, and then US is delivered. After a randomized inter-trial-interval, the next trial starts.

<img width="335" alt="image" src="https://github.com/user-attachments/assets/3698b5e2-cfdc-4498-9192-567f84d36013" />


We perform the experiments with the following 3 steps:
Stage1, CS3 - predicting reward at 90% probability
Stage2, CS1-3 - predicting reward at 10, 50, 90% probability, respectively.
Stage3, CS4 - predicting Airpuff at 90% probability.

<img width="707" alt="image" src="https://github.com/user-attachments/assets/7b366686-8f83-44d6-b3fe-4b2197c9f226" />


## Hardware overview
<img width="1287" alt="Screenshot 2025-04-30 at 3 27 46 PM" src="https://github.com/user-attachments/assets/5d8518ac-e1d8-4f38-b138-820322192ce5" />




## Installation
1. Clone this repo.
2. Navigate to the Bonsai folder and deploy Bonsai by executing the `setup.cmd` file.
3. Upload the 5Hz, 7Hz, 13kHz, and whitenoise SoundBinary files to the Ch#11, 12, 13, and 14 of the harp sound board, respectively.
4. Modify the `PavlovianSetting.csv` file under the SettingFiles folder accordingly (e.g. COM ports allocation for HARP boards) and then place the file under Documents.

For hardware installation and PC settings, please refer to the [DynamicForagingBehavior repo](https://github.com/AllenNeuralDynamics/dynamic-foraging-task).
For Fiber photometry installation, please refer to the [FIP_DAQ_Constorl repo](https://github.com/AllenNeuralDynamics/FIP_DAQ_Control).

## Usage
TBA


## Data organisation
The data will be saved into 3 modality folders.

<img width="145" alt="image" src="https://github.com/user-attachments/assets/fd9b5740-341f-40c0-b5e8-40e4cd69315a" />


### A.behavior
This folder contains raw HARP messages, a trial structure file ,a setting file, and TimeStamp csv files for events.

<img width="210" alt="image" src="https://github.com/user-attachments/assets/0e4c2cd7-a5f6-4b44-b062-7346009e1ee6" />

The trial structure file is organized as shown below.

<img width="373" alt="image" src="https://github.com/user-attachments/assets/23aeb29a-5e9f-432c-b21d-8880c4fe1f77" />

`TrialType` values correspond to0:

| TrialType | CS Type | Outcome     |
|-----------|---------|-------------|
| 1         | CS1     | Reward      |
| 2–10      | CS1     | No Reward   |
| 11–15     | CS2     | Reward      |
| 16–20     | CS2     | No Reward   |
| 21–29     | CS3     | Reward      |
| 30        | CS3     | No Reward   |
| 31–39     | CS4     | Airpuff     |
| 40        | CS4     | No Airpuff  |


TimeStamp csv files (`TS_*`) contains both `Windows softwareTS` and `HarpTS`

<img width="201" alt="image" src="https://github.com/user-attachments/assets/2e07f897-5b61-44dd-8d45-d5f1ad6aad4c" />





### B. behavior-videos
Please see [file standard repo](https://github.com/AllenNeuralDynamics/aind-file-standards/blob/main/file_formats/behavior_videos.md) and [DynamicForaging repo](https://github.com/AllenNeuralDynamics/dynamic-foraging-task)

### C. fib
Please see [file standard repo](https://github.com/AllenNeuralDynamics/aind-file-standards/blob/main/file_formats/fip.md) for details.
Briefly, the main csv data files for the Pavlovian sessions contain timestamps in `HarpTS` too in the last column of the data.

<img width="613" alt="image" src="https://github.com/user-attachments/assets/ef0fbfa5-323e-4cef-b7e6-64200a6dc61d" />









## Contributing
TBA
