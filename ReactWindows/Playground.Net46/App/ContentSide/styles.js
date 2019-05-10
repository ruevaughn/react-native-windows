import { StyleSheet } from 'react-native'

const content = {
  flexGrow: 1,
  flexDirection: 'column',
  backgroundColor: 'lightyellow',
  borderWidth: 1,
  borderColor: 'blanchedalmond'
}

const item = {
  flex: 1
}

const section = {}

section.title = {
  fontSize: 25
}

section.caption = {
  fontSize: 20
}

section.subCaption = {
  fontSize: 16,
  fontWeight: '500'
}

export default {content, item, section}
