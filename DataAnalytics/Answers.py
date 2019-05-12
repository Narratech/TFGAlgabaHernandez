import matplotlib as mpl
import matplotlib.pyplot as plt
import numpy as np

def DrawAnswerAnalytics(floatArray):

    xAxis = np.arange(0, len(floatArray), dtype = int)
    fig = plt.figure()
    plt.ylim(0,5)
    plt.bar(xAxis, floatArray, color ='red')
    print('Resultado del prcs: ', sum(floatArray))
    return fig
