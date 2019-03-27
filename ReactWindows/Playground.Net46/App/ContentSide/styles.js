import { StyleSheet } from 'react-native'

export default StyleSheet.create({
  content: {
    flexGrow: 1,
    backgroundColor: 'lightyellow',
    borderWidth: 1,
    borderColor: 'blanchedalmond'
  },
  textInput: {
    flexDirection: 'row',
    justifyContent: 'flex-start',
    alignItems: 'flex-start',
    borderWidth: 1,
    borderColor: 'blanchedalmond',
    margin: 2,
    height: 25,
  },
  autoCapOrdinar: {
    backgroundColor: 'lightyellow'
  },
  autoCapSelected: {
    borderWidth: 1,
    borderColor: 'maroon',
  }
})