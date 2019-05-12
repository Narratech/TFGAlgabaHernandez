import matplotlib as mpl
import matplotlib.pyplot as plt
import numpy as np

THRESHOLD = 0.001
def DrawSoundLoudness(floatArray, interval):
    steps = np.zeros(len(floatArray))
    i = 0
    silenceSteps = []
    silence = []
    for z in range(len(floatArray)):
        if(i > 0):
            steps[i] = steps[i-1]+ interval
            if(floatArray[i] < THRESHOLD):
                silenceSteps.append(steps[i-1] + interval)
                silence.append(floatArray[i])
        i = i +1
    
    
    desviacion = np.std(floatArray)
    media = np.mean(floatArray)
    mediana = np.median(floatArray)
    coeficiente_variacion = desviacion/media

    tiempoSilencio = (np.count_nonzero(silence))*interval
    print("Tiempo de silencio: ", '%.2f'%(tiempoSilencio))
    print("Desviación tipica: ", desviacion)
    #print("El coeficiente de variacion fué: ", (coeficiente_variacion*100), '%')

    fig = plt.figure()
    plt.plot(steps, floatArray, '-')
    plt.plot(silenceSteps, silence, '*', color= 'red')
    
    plt.plot(steps, [media for x in steps], '-', label = 'media')
    plt.plot(steps, [mediana for x in steps], '-', label = 'mediana')
    

    plt.xscale('linear')
    plt.ylim(0, 0.5)
    plt.ylabel('Sonoridad')
    plt.xlabel('Tiempo')
    plt.legend()
    plt.figtext(.5,.9,'Tiempo de silencio: ' + '%.2f'%(tiempoSilencio)+'s', fontsize=18, ha='center', label='silencio')
    return fig


def AnalizeSoundLoudness(floatArray, step):
    return




