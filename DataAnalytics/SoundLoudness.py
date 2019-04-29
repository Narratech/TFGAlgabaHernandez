import matplotlib as mpl
import matplotlib.pyplot as plt
import numpy as np

THRESHOLD = 0.00095
def DrawSoundLoudness(floatArray, interval):
    steps = np.zeros(len(floatArray))
    i = 0
    silenceSteps = []
    silence = []
    for z in range(len(floatArray)):
        if(i > 0):
            steps[i] = steps[i-1]+ interval
            if(floatArray[i] < THRESHOLD and floatArray[i-1] < THRESHOLD):
                silenceSteps.append(steps[i-1] + interval)
                silence.append(floatArray[i])
        i = i +1

    desviacion = np.std(floatArray)
    media = np.mean(floatArray)
    mediana = np.median(floatArray)
    coeficiente_variacion = desviacion/media

    tiempoSilencio = (np.count_nonzero(silence))*interval
    print("Tiempo de silencio: ", tiempoSilencio)
    #print("El coeficiente de variacion fué: ", (coeficiente_variacion*100), '%')

    fig = plt.figure()
    plt.plot(steps, floatArray, '-')
    plt.plot(silenceSteps, silence, '*', color= 'red')
    plt.plot(steps, [desviacion for x in steps], '-', label = "desviavion tipica")
    plt.plot(steps, [media for x in steps], '-', label = 'media')
    plt.plot(steps, [mediana for x in steps], '-', label = 'mediana')

    plt.ylabel('Valor medio cuadrático de sonido para el instante de tiempo')
    plt.xlabel('Tiempo')
    plt.figtext(.5,.9,'Tiempo de silencio: ' + str(tiempoSilencio), fontsize=18, ha='center')
    return fig


def AnalizeSoundLoudness(floatArray, step):
    return




