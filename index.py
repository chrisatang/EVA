import nltk
nltk.download('tagsets')

question1 = "Which Employee has the most sales?"

tokens = nltk.sent_tokenize(question1)
print(tokens)

word_tokens = nltk.word_tokenize(question1)
pos = nltk.pos_tag(word_tokens)
print(pos)

#Description of what each Part-of-Speech tag represents
nltk.help.upenn_tagset('VBZ')
