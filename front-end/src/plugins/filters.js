import Vue from 'vue'
import moment from 'moment'

Vue.filter('formatDate', function(value) {
  if (value) {
    return moment(String(value)).format('DD/MM/YYYY')
  }
})

Vue.filter('formatDateDDMM', function(value) {
  if (value) {
    return moment(String(value)).format('DD/MM')
  }
})

Vue.filter('formatDateLongerEN', function(value) {
  if (value) {
    return moment(String(value)).format('MMMM Do, YYYY')
  }
})

Vue.filter('formatTime', function(value) {
  if (value) {
    return moment(String(value)).format('HH:mm')
  }
})

Vue.filter('formatDateTime', function(value) {
  if (value) {
    return moment(String(value)).format('DD/MM/YYYY HH:mm')
  }
})
