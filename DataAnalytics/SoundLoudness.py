import matplotlib as mpl
import matplotlib.pyplot as plt
import numpy as np


def DrawSoundLoudness(floatArray, interval):
    steps = np.zeros(len(floatArray))
    i = 0

    for z in range(len(floatArray)):
        if(i > 0):
            steps[i] = steps[i-1]+ interval

        i = i +1
        
    plt.figure()
    plt.fill(steps, floatArray, '-')
    plt.ylabel('Valor medio cuadrático de sonido para el instante de tiempo')
    plt.legend()
    plt.savefig("Sonidoportiempo")
    