Vue.component('lazy-img', {
    template: "<img :src='url'>",
    props: {
        src: {
            type: String,
            required: false,
            default: () => {
                return this.url;
            }
        },
    }, 
    data() {
        return {
            url: 'loading.gif'
        }
    },
    watch: {
        src: {
            immediate: true,
            handler() {
                this.url = 'loading.gif'
                var newImg = new Image()
                newImg.src = this.src
                newImg.onerror = () => {
                    newImg.src = this.url
                }
                newImg.onload = () => {
                    this.url = newImg.src
                }
            }
        }
    },
    mounted() {

    }
})


new Vue({
    el: '#app',
    data() {
        return {
            types: [
                'DEFAULT', 'CHINESE', 'NUMBER', 'NUMBER_ZH_CN', 'NUMBER_ZH_HK', 'WORD', 'WORD_LOWER',
                'WORD_UPPER', 'WORD_NUMBER_LOWER', 'WORD_NUMBER_UPPER', 'ARITHMETIC', 'ARITHMETIC_ZH'
            ],
            fonts: ['Actionj', 'Fresnel', 'Kaiti', 'Prefix', 'Ransom', 'Scandal', 'Epilog', 'Headache', 'Lexo', 'Progbot', 'Robot'],
            groupCaptchas: [],
            textBold: true
        }
    },
    mounted() {
        this.generate()
    },
    methods: {
        handleChange() {
            this.generate()
        },
        generate() {
            let id = 0

            this.groupCaptchas = []
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
                        url: `captcha/dynamic?id=${++id}&type=${this.types[i]}&font=${this.fonts[j]}&textBold=${this.textBold}&t=${new Date}`,
                        isSupport: isSupport
                    })
                }
                this.groupCaptchas.push(typeCaptchas)
            }
        },
        isSupport(type, font) {
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