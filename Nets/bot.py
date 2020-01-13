import random
from telebot import types

import config
import telebot

bot = telebot.TeleBot(config.token)
rockList = ['https://www.youtube.com/watch?v=cTC1TEVo3Aw',
            'https://www.youtube.com/watch?v=2-ptLktOjrY',
            'https://www.youtube.com/watch?v=btPJPFnesV4',
            'https://www.youtube.com/watch?v=StZcUAPRRac',
            'https://www.youtube.com/watch?v=CJ2u3pRpCjc',
            'https://www.youtube.com/watch?v=1mjlM_RnsVE',
            'https://www.youtube.com/watch?v=GRiC35zeziU',
            'https://www.youtube.com/watch?v=iywaBOMvYLI',
            'https://www.youtube.com/watch?v=LoheCz4t2xc',
            'https://www.youtube.com/watch?v=CSvFpBOe8eY',
            'https://www.youtube.com/watch?v=EZjevnnkA20',
            'https://www.youtube.com/watch?v=_AsPY1bQx70',
            'https://www.youtube.com/watch?v=4p6GWewmTYQ',
            'https://www.youtube.com/watch?v=l482T0yNkeo',
            'https://www.youtube.com/watch?v=pAgnJDJN4VA']


@bot.message_handler(commands=["start"])
def start(message):
    keyboard = types.ReplyKeyboardMarkup(row_width=1, resize_keyboard=True)
    button = types.KeyboardButton(text="I wanna rock!!!")
    keyboard.add(button)
    bot.send_message(message.chat.id, "Are you ready?!?", reply_markup=keyboard)


@bot.message_handler(content_types=["text"])
def send_clip(message):
    if message.text == "I wanna rock!!!":
        bot.send_message(message.chat.id, rockList[random.randint(0, len(rockList))])


if __name__ == '__main__':
    bot.infinity_polling()
