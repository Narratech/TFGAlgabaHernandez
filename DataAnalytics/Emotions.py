import matplotlib as mpl
import matplotlib.pyplot as plt
import numpy as np



def DrawEmotions(vokaArr):
    emotions = np.zeros(6) #neutral, happy, sad, angry and fear in that order
    i = 0
    while i < (len(vokaArr)-5) :
        j = 0
        while(j < 5):
            if(vokaArr[i+j] == -1):
                emotions[5] = emotions[5] + 1
            else :
                emotions[j] = emotions[j] + vokaArr[i+j]
            j = j+1
        i = i +5

    fig = plt.figure()
    barlist = plt.bar([0,1,2,3,4,5], emotions, 0.9)
    barlist[0].set_color('green')
    barlist[1].set_color('yellow')
    barlist[2].set_color('blue')
    barlist[3].set_color('red')
    barlist[4].set_color('purple')
    barlist[5].set_color('beige')

    return fig





