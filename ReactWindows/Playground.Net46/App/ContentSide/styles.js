import { StyleSheet } from 'react-native'

export default StyleSheet.create({
  content: {
    flexGrow: 1,
    flexDirection: 'column',
    backgroundColor: 'lightyellow',
    borderWidth: 1,
    borderColor: 'blanchedalmond'
  },
  item: {
    flex: 1
  },
  title: {
    fontSize: 25
  },
  caption: {
    fontSize: 20
  },
  subCaption: {
    fontSize: 16,
    fontWeight: '500'
  },
  autoCapOrdinar: {
    backgroundColor: 'lightyellow'
  },
  autoCapSelected: {
    borderWidth: 1,
    borderColor: 'maroon',
  },
  textInput: {
    flexDirection: 'row',
    justifyContent: 'flex-start',
    alignItems: 'flex-start',
    borderWidth: 1,
    borderColor: 'blanchedalmond',
    margin: 2
  },
  imageBox: {
    height: 32,
    width: 46
  }
})