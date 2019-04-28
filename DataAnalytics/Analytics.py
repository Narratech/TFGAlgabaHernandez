import numpy as np
import json
import SoundLoudness
import Emotions
import os
dataDir = './Data/'

while(os.path.exists(dataDir)):
    with open(dataDir + 'User1TypeSoundLoudness.json') as json_file:
        jso = json.load(json_file)
        floatArr = np.array(jso['data'])

with open(dataDir + 'User1TypeVokaturi.json') as voka_json:
    jso = json.load(voka_json)
    vokaturiArr = np.array(jso['data'])


step = 0.1
SoundLoudness.DrawSoundLoudness(floatArr, step)
Emotions.DrawEmotions(vokaturiArr)