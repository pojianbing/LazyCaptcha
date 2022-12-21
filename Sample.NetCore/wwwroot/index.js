new Vue({
  el: '#app',
  data () { 
    return {
      types: [
        'DEFAULT', 'CHINESE', 'NUMBER', 'NUMBER_ZH_CN', 'NUMBER_ZH_HK', 'WORD', 'WORD_LOWER',
        'WORD_UPPER', 'WORD_NUMBER_LOWER', 'WORD_NUMBER_UPPER', 'ARITHMETIC', 'ARITHMETIC_ZH'
      ],
      fonts: ['Actionj', 'Fresnel', 'Kaiti', 'Prefix', 'Ransom', 'Scandal', 'Epilog', 'Headache', 'Lexo', 'Progbot', 'Robot'],
      groupCaptchas: []
    }
  },
  mounted () {
    let id = 0

    for (let i = 0; i < this.types.length; i++) { 
      let typeCaptchas = {
        type: this.types[i],
        captchas: []
      }
      for (let j = 0; j < this.fonts.length; j++) { 
        let isSupport = this.isSupport(this.types[i], this.fonts[j])
        typeCaptchas.captchas.push({
          type: this.types[i],
          font: this.fonts[j],
          url: `captcha/dynamic?id=${++id}&type=${this.types[i]}&font=${this.fonts[j]}`,
          isSupport: isSupport
        })
      }
      this.groupCaptchas.push(typeCaptchas)
    }
  },
  methods: {
    isSupport (type, font) { 
      //类型为中文，仅支持Kaiti
      if ((type === 'CHINESE' || type === 'NUMBER_ZH_CN' || type === 'NUMBER_ZH_HK' || type === 'ARITHMETIC_ZH') && font !== 'Kaiti') return false
      // 其他个例
      if (type === 'ARITHMETIC' && font === 'Fresnel') return false
      if (type === 'ARITHMETIC' && font === 'Ransom') return false
      if (type === 'ARITHMETIC' && font === 'Progbot') return false

      return true
    }
  },
})