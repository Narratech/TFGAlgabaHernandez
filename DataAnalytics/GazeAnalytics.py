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

    for z in range(len(gaze1)):
        if(i > 0):
            steps[i] = steps[i-1]+ interval
        i = i +1

    fig = plt.figure()

    plt.plot(steps, gaze1, '-', color = 'blue', label = 'Mirando a publisher 1')
    plt.plot(steps, gaze2, '-', color = 'red', label = 'Mirando a publisher 2')
    plt.plot(steps, gaze3, '-', color = 'green', label = 'Mirando a publisher 3')
    plt.legend()

    return fig
