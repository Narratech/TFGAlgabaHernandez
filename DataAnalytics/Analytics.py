import numpy as np
import json
import SoundLoudness
import Emotions
import HandAnalytics
import GazeAnalytics
import os
import matplotlib as mpl
import matplotlib.pyplot as plt


dataDir = '../Assets/RecordedData/'

i = 0
step = 0.1

soundArray = []
vokaturiArr = []

for filename in os.listdir(dataDir):
    if(filename.endswith('.json')):
        fileN = filename.split('.')[0] 
        with open(dataDir+filename) as json_file:
            jso = json.load(json_file)
            if('info' not in jso): 
                print("A")
            dataType = jso['info']
            fig = None
            if(dataType ==  'Sound'):
                soundArray = np.array(jso['data'])
                fig = SoundLoudness.DrawSoundLoudness(soundArray, 0.05)

            elif (dataType ==  'Vokaturi'):
                vokaturiArr = np.array(jso['data'])
                fig = Emotions.DrawEmotions(vokaturiArr)     

            elif (dataType == 'LeftHand' or dataType == 'RightHand'):
                #dataArray = np.array(jso['data'])
                #fig = HandAnalytics.DrawHandAnalytics(dataArray, step)
                ss = 0

            elif (dataType == 'Gazes'):
                dataArray = np.array(jso['data'])
                fig = GazeAnalytics.DrawGazeAnalytics(dataArray,step)

            else:
                dataArray = np.array(jso['data'])
                fig = SoundLoudness.DrawSoundLoudness(soundArray, step)

            if(fig is not None):
                fig.suptitle(fileN)
                fig.savefig('./Images/'+fileN+ '_chart')
                plt.close(fig)


