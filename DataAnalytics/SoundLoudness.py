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
    


    desviacion = np.std(floatArray)
    media = np.mean(floatArray)
    mediana = np.median(floatArray)
    coeficiente_variacion = desviacion/media
    #print("El coeficiente de variacion fué: ", (coeficiente_variacion*100), '%')

    fig = plt.figure()
    plt.plot(steps, floatArray, '-')
    plt.plot(steps, [desviacion for x in steps], '-', label = "desviavion tipica")
    plt.plot(steps, [media for x in steps], '-', label = 'media')
    plt.plot(steps, [mediana for x in steps], '-', label = 'mediana')

    plt.ylabel('Valor medio cuadrático de sonido para el instante de tiempo')
    plt.legend()
    return fig


def AnalizeSoundLoudness(floatArray, step):
    return




