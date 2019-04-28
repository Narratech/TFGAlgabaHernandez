import numpy as np
import json
import SoundLoudness
import Emotions
import os
dataDir = './Data/'

i = 0
step = 0.1

direc = dataDir+'User'+str(i)+'TypeSoundLoudness.json'
while(os.path.exists(direc)):
    with open(direc) as json_file:
        jso = json.load(json_file)
        floatArr = np.array(jso['data'])
    
    fig = SoundLoudness.DrawSoundLoudness(floatArr, step)
    fig.savefig('SoundLoudness'+ str(i))
    i = i+1
    direc = dataDir+'User'+str(i)+'TypeSoundLoudness.json'
     

with open(dataDir + 'User1TypeVokaturi.json') as voka_json:
    jso = json.load(voka_json)
    vokaturiArr = np.array(jso['data'])


Emotions.DrawEmotions(vokaturiArr)
