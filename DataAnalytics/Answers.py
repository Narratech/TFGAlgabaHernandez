import matplotlib as mpl
import matplotlib.pyplot as plt
import numpy as np

def DrawAnswerAnalytics(floatArray):
    xAxis = np.arange(0, len(floatArray), dtype = int)
    fig = plt.figure()
    plt.ylim(0,5)

    floatArray = 6 - floatArray
    aux = [0, 5, 7, 9, 10, 11] #preguntas que se tienen que invertir
    floatArray[aux] = 6 - floatArray[aux]

    plt.bar(xAxis, floatArray, color ='red')
    print('Resultado del PRCS: ', sum(floatArray))
    return fig
