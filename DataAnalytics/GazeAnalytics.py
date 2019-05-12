import matplotlib as mpl
import matplotlib.pyplot as plt
import numpy as np


def DrawGazeAnalytics(floatArray, interval):
    if(len(floatArray) < 2):
        print ("Invalid data array")
        return None
    gaze1 = np.array(floatArray[::3])
    gaze2 = np.array(floatArray[1:][::3])
    gaze3 = np.array(floatArray[2:][::3])

    steps = np.zeros(len(gaze1))
    i = 0

    barwidth = 0.9
    for z in range(len(gaze1)):
        if(i > 0):
            steps[i] = steps[i-1]+ interval
        i = i +1

    tGaze1 = sum(gaze1/100)
    tGaze2 = sum(gaze2/100)
    tGaze3 = sum(gaze3/100)

    print('Ha mirado a publisher 1 durante: ', tGaze1)
    print('Ha mirado a publisher 2 durante: ', tGaze2) 
    print('Ha mirado a publisher 3 durante: ', tGaze3) 
    print('Tiempo mirando a publishers: ', (tGaze1 + tGaze2 + tGaze3))


    fig = plt.figure()

    plt.bar(steps, gaze1, barwidth, alpha = 0.3, color = 'blue', label = 'Mirando a publisher 1')
    plt.bar(steps, gaze2, barwidth, alpha = 0.3, color = 'red', label = 'Mirando a publisher 2')
    plt.bar(steps, gaze3,barwidth,  alpha = 0.3, color = 'green', label = 'Mirando a publisher 3')
    plt.legend()

    return fig
